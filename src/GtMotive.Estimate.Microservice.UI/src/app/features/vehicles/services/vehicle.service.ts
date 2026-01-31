import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Vehicle, CreateVehicleRequest, RentVehicleRequest, ReturnVehicleRequest } from '../models/vehicle.model';

@Injectable({
    providedIn: 'root'
})
export class VehicleService {
    private apiUrl = '/api/Vehicles';

    constructor(private http: HttpClient) { }

    // Future: This should call this.http.get<Vehicle[]>(this.apiUrl)
    // For now, we only have getAvailable, so we check availability based on that.
    getVehicles(): Observable<Vehicle[]> {
        return this.http.get<Vehicle[]>(this.apiUrl).pipe(
            catchError(error => {
                console.error('Error fetching vehicles', error);
                return of([]);
            })
        );
    }

    createVehicle(vehicle: CreateVehicleRequest): Observable<any> {
        return this.http.post(this.apiUrl, vehicle);
    }

    rentVehicle(request: RentVehicleRequest): Observable<any> {
        return this.http.post(`${this.apiUrl}/rent`, request);
    }

    returnVehicle(request: ReturnVehicleRequest): Observable<any> {
        return this.http.post(`${this.apiUrl}/return`, request);
    }

    isAvailable(vehicle: Vehicle): boolean {
        return vehicle.isAvailable;
    }
}
