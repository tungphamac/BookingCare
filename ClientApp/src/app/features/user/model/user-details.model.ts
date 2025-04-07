export interface UserDetailsVm {
    userId: number;
    username: string;
    email: string;
    lockoutEnd: string  | null;
    lockoutEnabled: boolean;
  }
  