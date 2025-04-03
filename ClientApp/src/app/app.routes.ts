import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
import { TopClinicListComponent } from './features/clinic/top-clinic-list/top-clinic-list.component';
import { FeedbackListComponent } from './features/feedback/feedback-list/feedback-list.component';
import { FeedbackAddComponent } from './features/feedback/feedback-add/feedback-add.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/register/register/register.component';
import { ForgotPasswordComponent } from './features/ForgotPassword/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './features/ResetPassword/reset-password/reset-password.component';
import { ClinicDetailComponent } from './features/clinic/clinic-detail/clinic-detail.component';
import { DoctorDetailComponent } from './features/doctor/doctorc-detail/doctorc-detail.component';
import { PatientDetailComponent } from './features/patient/patient-detail/patient-detail.component';
import { SearchComponent } from './features/search/search/search.component';
import { PatientSearchComponent } from './features/search/patient-search/patient-search.component';
import { DoctorProfileComponent } from './features/doctor/doctor-profile/doctor-profile.component';
import { ClinicProfileComponent } from './features/clinic/clinic-profile/clinic-profile.component';
import { NotificationListComponent } from './features/notification/notification-list/notification-list.component'; // Thêm import

export const routes: Routes = [
  { path: '', component: HomepageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'clinics/top-clinic', component: TopClinicListComponent },
  { path: 'clinics/:id', component: ClinicDetailComponent },
  { path: 'doctors/:id', component: DoctorDetailComponent },
  { path: 'feedbacks/list', component: FeedbackListComponent },
  { path: 'feedbacks/add', component: FeedbackAddComponent },
  { path: 'search', component: SearchComponent },
  { path: 'patient-search', component: PatientSearchComponent },
  { path: 'doctor-profile/:id', component: DoctorProfileComponent },
  { path: 'clinic-profile/:id', component: ClinicProfileComponent },
  { path: 'patients/:id', component: PatientDetailComponent },
  { path: 'notifications', component: NotificationListComponent } // Thêm route để test
];