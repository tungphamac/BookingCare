
import { Component, OnInit } from '@angular/core';
import { TopClinicListComponent } from "../../../features/clinic/top-clinic-list/top-clinic-list.component";
// import { TopDoctorListComponent } from "../../../features/doctor/top-doctor-list/top-doctor-list.component";
import { TopSpecializationListComponent } from "../../../features/specialization/top-specialization-list/top-specialization-list.component";
// import { TopRatingDoctorListComponent } from '../../../features/doctor/top-rating-doctor-list/top-rating-doctor-list.component';

@Component({
  selector: 'app-homepage',
  imports: [TopClinicListComponent, TopSpecializationListComponent,],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
