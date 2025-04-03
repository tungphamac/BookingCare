export interface LoginResponse {
    token: string;
    email: string;
    id: number;
    role: string; // Vai trò: "Patient", "Doctor", hoặc "Unknown"
}