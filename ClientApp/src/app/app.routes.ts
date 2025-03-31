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
import { CreateAppointmentComponent } from './features/appointment/components/create-appointment/create-appointment.component';
import { ManageAppointmentComponent } from './features/appointment/components/manage-appointment/manage-appointment.component';
import { UpdateAppointmentComponent } from './features/appointment/components/update-appointment/update-appointment.component';
import { ViewAppointmentDetailComponent } from './features/appointment/components/view-appointment-detail/view-appointment-detail.component';
import { ManageMedicalRecordComponent } from './features/medicalRecord/component/manage-medical-record/manage-medical-record.component';

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

    { path: '', component: HomepageComponent },
    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add', component: FeedbackAddComponent },
    { path: 'doctors/profile', component: DoctorProfileComponent }

];
