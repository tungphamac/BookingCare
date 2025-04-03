import { Component } from '@angular/core';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-add-patient',
  templateUrl: './add-patient.component.html'
})
export class AddPatientComponent {
  patients: any[] = []; // Mảng để quản lý danh sách bệnh nhân

  constructor(private patientService: PatientService) { }

  addPatient(): void {
    const form = document.getElementById('addPatientForm') as HTMLFormElement;
    const userName = (document.getElementById('userName') as HTMLInputElement).value;
    const email = (document.getElementById('email') as HTMLInputElement).value;
    const password = (document.getElementById('password') as HTMLInputElement).value;
    const gender = (document.getElementById('gender') as HTMLSelectElement).value === 'true';
    const address = (document.getElementById('address') as HTMLInputElement).value;
    const avatar = (document.getElementById('avatar') as HTMLInputElement).value;
    const medicalHistory = (document.getElementById('medicalHistory') as HTMLInputElement).value;;

    if (form.checkValidity()) {
      const newPatient = { userName, email, password, gender, address, avatar, medicalHistory };
      this.patientService.addPatient(newPatient).subscribe({
        next: (patient) => {
          this.patients.push(patient); // Giả sử response trả về bệnh nhân mới được thêm
          alert('Patient added successfully');
        },
        error: (err) => alert('Error adding patient: ' + JSON.stringify(err))
      });
    } else {
      form.reportValidity();
    }
  }

  deletePatient(id: number): void {
    if (confirm("Are you sure to delete this patient?")) {
      this.patientService.deletePatient(id).subscribe({
        next: () => {
          this.patients = this.patients.filter(patient => patient.id !== id);
          alert('Patient deleted successfully');
        },
        error: (err) => {
          console.error('Error deleting patient:', err);
          alert('Failed to delete patient');
        }
      });
    }
  }
}
