
import { Component, OnInit } from '@angular/core';
import { TopClinicListComponent } from "../../../features/clinic/top-clinic-list/top-clinic-list.component";
import { TopDoctorListComponent } from "../../../features/doctor/top-doctor-list/top-doctor-list.component";
import { TopSpecializationListComponent } from "../../../features/specialization/top-specialization-list/top-specialization-list.component";
import { TopRatingDoctorListComponent } from '../../../features/doctor/top-rating-doctor-list/top-rating-doctor-list.component';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-homepage',
  imports: [TopClinicListComponent, TopDoctorListComponent, TopSpecializationListComponent, TopRatingDoctorListComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {
  videoUrl: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {
    this.videoUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://www.youtube.com/embed/FyDQljKtWnI?autoplay=0&enablejsapi=1&origin=https%3A%2F%2Fbookingcare.vn&widgetid=1&forigin=https%3A%2F%2Fbookingcare.vn%2F&aoriginsup=1&gporigin=https%3A%2F%2Fbookingcare.vn%2Ftimkiem&vf=1'
    );
  }
}
