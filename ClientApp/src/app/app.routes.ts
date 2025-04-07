import { Routes } from '@angular/router';
import { HomepageComponent } from './core/components/homepage/homepage.component';
import { TopClinicListComponent } from './features/clinic/top-clinic-list/top-clinic-list.component';
import { FeedbackListComponent } from './features/feedback/feedback-list/feedback-list.component';
import { FeedbackAddComponent } from './features/feedback/feedback-add/feedback-add.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/register/register/register.component';
import { ForgotPasswordComponent } from './features/ForgotPassword/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './features/ResetPassword/reset-password/reset-password.component';
import { FeedbackViewComponent } from './features/feedback/feedback-view/feedback-view.component';
import { ViewAppointmentDetailComponent } from './features/appointment/components/view-appointment-detail/view-appointment-detail.component';
import { UpdateAppointmentComponent } from './features/appointment/components/update-appointment/update-appointment.component';
import { CreateAppointmentComponent } from './features/appointment/components/create-appointment/create-appointment.component';
import { ManageAppointmentComponent } from './features/appointment/components/manage-appointment/manage-appointment.component';
import { ClinicDetailComponent } from './features/clinic/clinic-detail/clinic-detail.component';
import { DoctorDetailComponent } from './features/doctor/doctorc-detail/doctorc-detail.component';
import { SearchComponent } from './features/search/search/search.component';
import { PatientSearchComponent } from './features/search/patient-search/patient-search.component';
import { DoctorProfileComponent } from './features/doctor/doctor-profile/doctor-profile.component';
import { ClinicProfileComponent } from './features/clinic/clinic-profile/clinic-profile.component';
import { NotificationListComponent } from './features/notification/notification-list/notification-list.component';
import { PatientDetailComponent } from './features/patient/patientc-detail/patientc-detail.component';
import { ScheduleManagementComponent } from './features/schedule/components/manage-schedule/manage-schedule.component';
import { SpecializationDetailComponent } from './features/specialization/components/specialization-detail/specialization-detail.component';
import { ListDoctorComponent } from './features/doctor/list-doctor/list-doctor.component';
import { AddDoctorComponent } from './features/doctor/add-doctor/add-doctor.component';
import { SpecializationListComponent } from './features/specialization/components/specialization-list/specialization-list.component';
import { PatientsComponent } from './features/patient/patient.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { PatientEditComponent } from './features/patient/edit-patient/edit-patient.component';
import { ClinicListComponent } from './features/clinic/clinic.component';
import { AddClinicComponent } from './features/clinic/add-clinic/add-clinic.component';
import { ClinicEditComponent } from './features/clinic/edit-clinic/edit-clinic.component';
import { AddSpecializationComponent } from './features/specialization/add-specialization/add-specialization.component';
import { EditSpecializationComponent } from './features/specialization/edit-specialization/edit-specialization.component';
import { SpecializationComponent } from './features/specialization/specialization.component';

import { ChatComponent } from './features/chat/chat/chat.component';

import { CreateMedicalRecordComponent } from './features/medicalRecord/components/create-medical-record/create-medical-record.component';
import { ViewMedicalRecordComponent } from './features/medicalRecord/components/view-medical-record/view-medical-record.component';
import { UpdateMedicalRecordComponent } from './features/medicalRecord/components/update-medical-record/update-medical-record.component';


export const routes: Routes = [
  { path: '', component: HomepageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'appointments/create', component: CreateAppointmentComponent },
  { path: 'appointments', component: ManageAppointmentComponent },
  { path: 'appointments/update/:id', component: UpdateAppointmentComponent },
  { path: 'appointments/:id', component: ViewAppointmentDetailComponent },
  { path: 'medical-records/create', component: CreateMedicalRecordComponent },
  { path: 'medical-records/:id', component: ViewMedicalRecordComponent },
  { path: 'medical-records/update/:id', component: UpdateMedicalRecordComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },


    { path: 'patients/:id', component: PatientDetailComponent },

    { path: 'clinics/:id', component: ClinicDetailComponent },
   { path: 'doctors/detail/:id', component: DoctorDetailComponent },
    { path: 'search', component: SearchComponent },
    { path: 'patient-search', component: PatientSearchComponent },
    { path: 'clinic-profile/:id', component: ClinicProfileComponent },
    { path: 'patients/:id', component: PatientDetailComponent },
    

    { path: 'admin/get-all-doctors', component: ListDoctorComponent },
    { path: 'admin/add-doctor', component: AddDoctorComponent },
    { path: 'admin/get-all-patients', component: PatientsComponent },
    { path: 'admin/add-patient', component: AddPatientComponent },
    //{ path: 'Patient/update/:id', component: PatientEditComponent },
    { path: 'Clinic/getall', component: ClinicListComponent },
    { path: 'Clinic/add', component: AddClinicComponent },
    { path: 'Clinic/update/:id', component: ClinicEditComponent },
    { path: 'Specialization/getall', component: SpecializationComponent },
    { path: 'Specialization/add', component: AddSpecializationComponent },
    { path: 'Specialization/update/:id', component: EditSpecializationComponent },
    { path: 'clinics/top-clinic', component: TopClinicListComponent },
    { path: 'feedbacks/list', component: FeedbackListComponent },
    { path: 'feedbacks/add/:appointmentId', component: FeedbackAddComponent },
    { path: 'doctors/profile/:doctorId', component: DoctorProfileComponent },
    { path: 'feedbacks/view/:doctorId', component: FeedbackViewComponent },
  { path: 'feedbacks/add', component: FeedbackAddComponent },
  { path: 'doctor-profile/:id', component: DoctorProfileComponent },
  // Routes cho Schedule
  { path: 'schedules', component: ScheduleManagementComponent }, // Danh sách schedules theo doctorId qua query param
  { path: 'schedules/:id', component: ScheduleManagementComponent }, // Xem chi tiết schedule theo ID
  { path: 'schedules/create', component: ScheduleManagementComponent }, // Thêm schedule
  { path: 'schedules/update/:id', component: ScheduleManagementComponent }, // Sửa schedule

  { path: 'specializations', component: SpecializationListComponent },
  { path: 'specializations/:id', component: SpecializationDetailComponent },
  { path: 'chat', component: ChatComponent },



];
