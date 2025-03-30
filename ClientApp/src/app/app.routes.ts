import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
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
];
