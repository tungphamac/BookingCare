import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
import { TopClinicListComponent } from './features/clinic/top-clinic-list/top-clinic-list.component';
import { FeedbackListComponent } from './features/feedback/feedback-list/feedback-list.component';
import { FeedbackAddComponent } from './features/feedback/feedback-add/feedback-add.component';
import { DoctorProfileComponent } from './features/doctor/doctor-profile/doctor-profile.component';

export const routes: Routes = [
    { path: '', component: HomepageComponent },
    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add', component: FeedbackAddComponent },
    { path: 'doctors/profile', component: DoctorProfileComponent }
];
