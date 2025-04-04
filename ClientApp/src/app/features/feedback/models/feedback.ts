export interface Feedback {
    id: number;
    patientName: string;
    appointmentId: number;
    rating: number;
    comment: string;
    createAt: Date;
}