<<<<<<< HEAD
import { Component } from '@angular/core';

@Component({
  selector: 'app-homepage',
  imports: [],
=======
import { Component, OnInit } from '@angular/core';
import { TopClinicListComponent } from "../../../features/clinic/top-clinic-list/top-clinic-list.component";
import { TopDoctorListComponent } from "../../../features/doctor/top-doctor-list/top-doctor-list.component";
import { TopSpecializationListComponent } from "../../../features/specialization/top-specialization-list/top-specialization-list.component";
import { TopRatingDoctorListComponent } from '../../../features/doctor/top-rating-doctor-list/top-rating-doctor-list.component';

@Component({
  selector: 'app-homepage',
  imports: [TopClinicListComponent, TopDoctorListComponent, TopSpecializationListComponent, TopRatingDoctorListComponent],
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
