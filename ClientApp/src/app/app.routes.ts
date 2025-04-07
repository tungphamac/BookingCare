import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';


import { TopClinicListComponent } from './features/clinic/top-clinic-list/top-clinic-list.component';
import { FeedbackListComponent } from './features/feedback/feedback-list/feedback-list.component';
import { FeedbackAddComponent } from './features/feedback/feedback-add/feedback-add.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/register/register/register.component';
import { ForgotPasswordComponent } from './features/ForgotPassword/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './features/ResetPassword/reset-password/reset-password.component';

import { ManageMedicalRecordComponent } from './features/medicalRecord/component/manage-medical-record/manage-medical-record.component';
import { ViewAppointmentDetailComponent } from './features/appointment/components/view-appointment-detail/view-appointment-detail.component';
import { UpdateAppointmentComponent } from './features/appointment/components/update-appointment/update-appointment.component';
import { CreateAppointmentComponent } from './features/appointment/components/create-appointment/create-appointment.component';
import { ManageAppointmentComponent } from './features/appointment/components/manage-appointment/manage-appointment.component';

import { ClinicDetailComponent } from './features/clinic/clinic-detail/clinic-detail.component';


import { SearchComponent } from './features/search/search/search.component';
import { PatientSearchComponent } from './features/search/patient-search/patient-search.component';

import { ClinicProfileComponent } from './features/clinic/clinic-profile/clinic-profile.component';
import { NotificationListComponent } from './features/notification/notification-list/notification-list.component'; // Thêm import
import { PatientDetailComponent } from './features/patient/patientc-detail/patientc-detail.component';



import { PatientsComponent } from './features/patient/patient.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { PatientEditComponent } from './features/patient/edit-patient/edit-patient.component';
import { ClinicListComponent } from './features/clinic/clinic.component';
import { ClinicCreateComponent } from './features/clinic/add-clinic/add-clinic.component';
import { ClinicEditComponent } from './features/clinic/edit-clinic/edit-clinic.component';
import { SpecializationListComponent } from './features/specialization/specialization.component';
import { AddSpecializationComponent } from './features/specialization/add-specialization/add-specialization.component';
import { EditSpecializationComponent } from './features/specialization/edit-specialization/edit-specialization.component';
import { UserManagementComponent } from './features/user/user.component';
import { DoctorListComponent} from './features/doctor/list-doctor/list-doctor.component';
import { AddDoctorComponent } from './features/doctor/add-doctor/add-doctor.component';



export const routes: Routes = [
    { path: '', component: HomepageComponent },



    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'appointments/create',
        component: CreateAppointmentComponent
    },
    {
        path: 'appointments',
        component: ManageAppointmentComponent
    },
    {
        path: 'appointments/update/:id',
        component: UpdateAppointmentComponent
    },
    {
        path: 'appointments/:id',
        component: ViewAppointmentDetailComponent
    },
    {
        path: 'medical-records/create',
        component: ManageMedicalRecordComponent
    },
    {
        path: 'medical-records/:id',
        component: ManageMedicalRecordComponent
    },
    {
        path: 'forgot-password',
        component: ForgotPasswordComponent
    },

    {
        path: 'reset-password',
        component: ResetPasswordComponent
    },



    { path: 'patients/:id', component: PatientDetailComponent },

    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'clinics/:id', component: ClinicDetailComponent },

    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add', component: FeedbackAddComponent },
    { path: 'search', component: SearchComponent },
    { path: 'patient-search', component: PatientSearchComponent },

    { path: 'clinic-profile/:id', component: ClinicProfileComponent },
    { path: 'patients/:id', component: PatientDetailComponent },
    { path: 'notifications', component: NotificationListComponent }, // Thêm route để test


    { path: 'Patient/getall', component: PatientsComponent },
    { path: 'admin/add-patient', component: AddPatientComponent },
    //{ path: 'Patient/update/:id', component: PatientEditComponent },
    { path: 'Clinic/get-all-clinics', component: ClinicListComponent },
    { path: 'Clinic/add', component: ClinicCreateComponent },
    { path: 'Clinic/update/:id', component: ClinicEditComponent },
    { path: 'Specialization/get-all-specializations', component: SpecializationListComponent },
    { path: 'Specialization/add', component: AddSpecializationComponent },
    { path: 'Specialization/update/:id', component: EditSpecializationComponent },
    { path: 'Account/getall', component: UserManagementComponent },
    { path: 'Doctor/getall', component: DoctorListComponent },
    { path: 'Doctor/add-doctor', component: AddDoctorComponent } // Thêm route cho AddDoctor
];
