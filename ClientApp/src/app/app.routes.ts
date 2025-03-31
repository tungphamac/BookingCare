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




    { path: '', component: HomepageComponent },
    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add', component: FeedbackAddComponent },
    { path: 'doctors/profile', component: DoctorProfileComponent }

];
