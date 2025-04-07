import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ClinicService } from '../services/clinic.service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-top-clinic-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './top-clinic-list.component.html',
  styleUrl: './top-clinic-list.component.css'
})
export class TopClinicListComponent implements OnInit {
  @ViewChild('scrollContainer', { static: false }) scrollContainer!: ElementRef;
  clinics: any[] = [];

  constructor(private clinicService: ClinicService) { }

  ngOnInit(): void {
    this.clinicService.getTopClinics().subscribe({
      next: response => {
        this.clinics = response;
        console.log(this.clinics);
      }
    });
  }

  scrollLeft(): void {
    this.scrollContainer.nativeElement.scrollBy({ left: -300, behavior: 'smooth' });
  }

  scrollRight(): void {
    this.scrollContainer.nativeElement.scrollBy({ left: 300, behavior: 'smooth' });
  }
}
