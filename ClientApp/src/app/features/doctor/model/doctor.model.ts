// src/app/models/doctor.model.ts
export interface Doctor {
    id: number;
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
  // src/app/models/doctor.model.ts

export class AddDoctor {
    userName: string;
    email: string;
    password: string;
    gender: boolean; // true for male, false for female
    address: string;
    avatar: string;
    achievement: string;
    description: string;
    specializationId: number;
    clinicId: number;
  
    constructor(
      userName: string = '',
      email: string = '',
      password: string = '',
      gender: boolean = true,
      address: string = '',
      avatar: string = '',
      achievement: string = '',
      description: string = '',
      specializationId: number = 0,
      clinicId: number = 0
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
  