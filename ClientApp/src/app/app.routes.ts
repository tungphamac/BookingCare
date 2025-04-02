import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
import { TopClinicListComponent } from './features/clinic/top-clinic-list/top-clinic-list.component';
import { FeedbackListComponent } from './features/feedback/feedback-list/feedback-list.component';
import { FeedbackAddComponent } from './features/feedback/feedback-add/feedback-add.component';
import { DoctorProfileComponent } from './features/doctor/doctor-profile/doctor-profile.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/register/register/register.component';
import { ForgotPasswordComponent } from './features/ForgotPassword/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './features/ResetPassword/reset-password/reset-password.component';
import { ViewAppointmentDetailComponent } from './features/appointment/components/view-appointment-detail/view-appointment-detail.component';
import { UpdateAppointmentComponent } from './features/appointment/components/update-appointment/update-appointment.component';
import { ManageAppointmentComponent } from './features/appointment/components/manage-appointment/manage-appointment.component';
import { CreateAppointmentComponent } from './features/appointment/components/create-appointment/create-appointment.component';
import { ViewMedicalRecordComponent } from './features/medicalRecord/components/view-medical-record/view-medical-record.component';
import { CreateMedicalRecordComponent } from './features/medicalRecord/components/create-medical-record/create-medical-record.component';
import { UpdateMedicalRecordComponent } from './features/medicalRecord/components/update-medical-record/update-medical-record.component';

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
        path: 'forgot-password',
        component: ForgotPasswordComponent
    },

    {
        path: 'reset-password',
        component: ResetPasswordComponent
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
        component: CreateMedicalRecordComponent 
    },
    { 
        path: 'medical-records/:id', 
        component: ViewMedicalRecordComponent 
    },
    { 
        path: 'medical-records/update/:id', 
        component: UpdateMedicalRecordComponent 
    },




    { path: '', component: HomepageComponent },
    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add', component: FeedbackAddComponent },
    { path: 'doctors/profile', component: DoctorProfileComponent }

];
