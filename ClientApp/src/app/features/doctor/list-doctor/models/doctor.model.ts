export class Doctor {
  userId?: number;
  userName?: string;
  email?: string;
  gender?: boolean;
  address?: string;
    avatar?: string;
    achievement?: string;
    description?: string;
    specializationName?: string;
    clinicName?: string;
    isBanned?: boolean; // Trạng thái bị cấm
    isLocked?: boolean; // Trạng thái bị khóa
  }
  export class CreateDoctorDto {
    userName: string;
    email: string;
    password: string; // Thêm mật khẩu
    gender: boolean;
    address: string;
    avatar: string;
    achievement: string;
    description: string;
    specializationId: number;
    clinicId: number;
  
    constructor(
      userName: string,
      email: string,
      password: string,
      gender: boolean,
      address: string,
      avatar: string,
      achievement: string,
      description: string,
      specializationId: number,
      clinicId: number
    ) {
      this.userName = userName;
      this.email = email;
      this.password = password;
      this.gender = gender;
      this.address = address;
      this.avatar = avatar;
      this.achievement = achievement;
      this.description = description;
      this.specializationId = specializationId;
      this.clinicId = clinicId;
    }
  }
  export interface editDoctor {
    userName: string;
    email: string;
    gender: boolean;
    address: string;
    avatar: string;
    achievement: string;
    description: string;
    specializationId: number;
    clinicId: number;
  }
  
