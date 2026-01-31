export interface Vehicle {
    id: string; // Guid
    brand: string;
    model: string;
    licensePlate: string;
    manufacturingDate: string; // ISO Date
    isAvailable: boolean;
}

export interface CreateVehicleRequest {
    brand: string;
    model: string;
    licensePlate: string;
    manufacturingDate: string;
}

export interface RentVehicleRequest {
    vehicleId: string;
    customerId: string;
}

export interface ReturnVehicleRequest {
    vehicleId: string;
}
