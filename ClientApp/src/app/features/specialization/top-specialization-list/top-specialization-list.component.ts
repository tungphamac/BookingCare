import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopSpecialization } from '../models/top-specialization.model';
import { SpecializationService } from '../services/specialization.service';

@Component({
  selector: 'app-top-specialization-list',
  imports: [CommonModule],
  templateUrl: './top-specialization-list.component.html',
  styleUrl: './top-specialization-list.component.css'
})
export class TopSpecializationListComponent implements OnInit {
  specializations: TopSpecialization[] = [];

  constructor(private specializationService: SpecializationService) { }

  ngOnInit(): void {
    this.specializationService.getTopSpecializations().subscribe({
      next: response => {
        this.specializations = response;
        console.log(this.specializations);
      }
    });
  }
}
