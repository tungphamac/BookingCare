export interface LoginResponse {
    role: string; //Them
    token: string;
    email: string;
    id: number;
    role: string; // Vai trò: "Patient", "Doctor", hoặc "Unknown"
}