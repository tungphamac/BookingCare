import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
import { ListDoctorComponent } from './features/doctor/list-doctor/list-doctor.component';
import { AddDoctorComponent } from './features/doctor/add-doctor/add-doctor.component';

import { PatientsComponent } from './features/patient/patient.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { PatientEditComponent } from './features/patient/edit-patient/edit-patient.component';
import { ClinicListComponent } from './features/clinic/clinic.component';
import { ClinicCreateComponent } from './features/clinic/add-clinic/add-clinic.component';
import { ClinicEditComponent } from './features/clinic/edit-clinic/edit-clinic.component';
import { SpecializationListComponent } from './features/specialization/specialization.component';
import { AddSpecializationComponent } from './features/specialization/add-specialization/add-specialization.component';
import { EditSpecializationComponent } from './features/specialization/edit-specialization/edit-specialization.component';


export const routes: Routes = [
    {path: '', component: HomepageComponent},
    { path: 'Doctor/getall', component: ListDoctorComponent },
    { path: 'Doctor/add-doctor', component: AddDoctorComponent },   
    {path:'Patient/getall', component: PatientsComponent},
    {path:'Patient/add', component:AddPatientComponent},
    { path: 'Patient/update/:id', component: PatientEditComponent },
    { path: 'Clinic/getall', component: ClinicListComponent },
    { path: 'Clinic/add', component: ClinicCreateComponent },
    { path: 'Clinic/update/:id', component: ClinicEditComponent },
    { path: 'Specialization/getall', component: SpecializationListComponent },
    { path: 'Specialization/add', component: AddSpecializationComponent },
    { path: 'Specialization/update/:id', component: EditSpecializationComponent },


];
