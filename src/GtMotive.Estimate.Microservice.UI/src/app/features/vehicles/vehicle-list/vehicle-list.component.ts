import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle } from '../models/vehicle.model';

// Custom validator for date rules (not future, max 5 years old)
export function dateValidator(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null;
    const date = new Date(control.value);
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    if (date > today) return { futureDate: true };

    const fiveYearsAgo = new Date(today);
    fiveYearsAgo.setFullYear(today.getFullYear() - 5);

    if (date < fiveYearsAgo) return { tooOld: true };

    return null;
}

@Component({
    selector: 'app-vehicle-list',
    standalone: true,
    imports: [CommonModule, RouterModule, ReactiveFormsModule],
    templateUrl: './vehicle-list.component.html',
    styleUrl: './vehicle-list.component.css'
})
export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[] = [];
    loading = true;

    // Inline Add Form
    isAdding = false;
    vehicleForm: FormGroup;
    submitting = false;

    // Error Handling
    errorMessage: string | null = null;
    successMessage: string | null = null;

    constructor(
        private vehicleService: VehicleService,
        private fb: FormBuilder
    ) {
        this.vehicleForm = this.fb.group({
            brand: ['', Validators.required],
            model: ['', Validators.required],
            licensePlate: ['', [Validators.required, Validators.pattern(/^[0-9]{4}[A-Za-z]{3}$/)]], // Example: 1234ABC
            manufacturingDate: ['', [Validators.required, dateValidator]]
        });
    }

    ngOnInit(): void {
        this.loadVehicles();
    }

    sortColumn = 'manufacturingDate';
    sortDirection: 'asc' | 'desc' = 'desc';

    loadVehicles(keepMessages = false): void {
        this.loading = true;
        if (!keepMessages) {
            this.clearMessages();
        }
        this.vehicleService.getVehicles().subscribe({
            next: (data) => {
                this.vehicles = data;
                this.sortData();
                this.loading = false;
            },
            error: (e) => {
                this.handleError(e);
                this.loading = false;
            }
        });
    }

    onSort(column: string): void {
        if (this.sortColumn === column) {
            this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.sortColumn = column;
            this.sortDirection = 'asc';
        }
        this.sortData();
    }

    private sortData(): void {
        this.vehicles.sort((a, b) => {
            // Primary Sort: Availability (Available First)
            if (a.isAvailable !== b.isAvailable) {
                return a.isAvailable ? -1 : 1;
            }

            // Secondary Sort: Selected Column
            const direction = this.sortDirection === 'asc' ? 1 : -1;
            let valA = (a as any)[this.sortColumn];
            let valB = (b as any)[this.sortColumn];

            if (typeof valA === 'string') valA = valA.toLowerCase();
            if (typeof valB === 'string') valB = valB.toLowerCase();

            if (valA < valB) return -1 * direction;
            if (valA > valB) return 1 * direction;
            return 0;
        });
    }

    rentVehicle(vehicle: Vehicle): void {
        const customerId = crypto.randomUUID();
        if (!this.vehicleService.isAvailable(vehicle)) return;

        this.vehicleService.rentVehicle({ vehicleId: vehicle.id, customerId }).subscribe({
            next: () => {
                this.showSuccess(`CustomerId ${customerId} 've rented the vehicle ${vehicle.id}`);
                this.loadVehicles(true);
            },
            error: (err) => this.handleError(err)
        });
    }

    returnVehicle(vehicle: Vehicle): void {
        this.clearMessages();
        this.vehicleService.returnVehicle({ vehicleId: vehicle.id }).subscribe({
            next: () => {
                this.showSuccess('Vehicle returned successfully');
                this.loadVehicles(true);
            },
            error: (err) => this.handleError(err)
        });
    }

    isAvailable(vehicle: Vehicle): boolean {
        return this.vehicleService.isAvailable(vehicle);
    }

    toggleAddMode(): void {
        this.clearMessages();
        this.isAdding = !this.isAdding;
        if (this.isAdding) {
            this.vehicleForm.reset();
        }
    }

    onSubmit(): void {
        if (this.vehicleForm.invalid) return;

        this.submitting = true;
        const vehicleData = {
            ...this.vehicleForm.value,
            licensePlate: this.vehicleForm.value.licensePlate.toUpperCase(),
            manufacturingDate: new Date(this.vehicleForm.value.manufacturingDate).toISOString()
        };

        this.clearMessages();
        this.vehicleService.createVehicle(vehicleData).subscribe({
            next: () => {
                this.submitting = false;
                this.isAdding = false;
                this.showSuccess('Vehicle created successfully');
                this.loadVehicles(true); // Reload list
            },
            error: (err) => {
                this.handleError(err);
                this.submitting = false;
            }
        });
    }

    // Message Timeout Helper
    private messageTimeout: any;

    showSuccess(message: string): void {
        this.clearMessages();
        this.successMessage = message;
        this.setAutoDismiss();
    }

    showError(message: string): void {
        this.clearMessages();
        this.errorMessage = message;
        this.setAutoDismiss();
    }

    private setAutoDismiss(): void {
        if (this.messageTimeout) {
            clearTimeout(this.messageTimeout);
        }
        this.messageTimeout = setTimeout(() => {
            this.clearMessages();
        }, 5000);
    }

    closeMessage(): void {
        this.clearMessages();
    }

    private handleError(error: any): void {
        console.error('API Error:', error);
        let msg = 'An unexpected error occurred';

        if (error.error && typeof error.error === 'string') {
            msg = error.error;
        } else if (error.error?.message) {
            msg = error.error.message;
        } else if (error.error?.detail) {
            msg = error.error.detail;
        } else if (error.message) {
            msg = error.message;
        }

        this.showError(msg);
    }

    private clearMessages(): void {
        this.errorMessage = null;
        this.successMessage = null;
        if (this.messageTimeout) {
            clearTimeout(this.messageTimeout);
            this.messageTimeout = null;
        }
    }
}
