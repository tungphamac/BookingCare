export interface Doctor {
  userId: number;
  userName: string;
  email: string;
  gender: boolean;
  address: string;
  avatar: string;
  achievement: string;
  description: string;
  specializationName: string;
  clinicName: string;
}

export interface GetDoctor {
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
