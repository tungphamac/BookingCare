export interface Doctor {
    id: number;
    name: string;
    gender: boolean;
    email: string;
    phone: number;
    address: string;
    avatar: File;
    achievement: string;
    description: string;
    specializationId: number;
    clinicId: number;
}