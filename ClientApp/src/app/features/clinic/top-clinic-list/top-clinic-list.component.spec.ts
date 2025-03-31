import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopClinicListComponent } from './top-clinic-list.component';

describe('TopClinicListComponent', () => {
  let component: TopClinicListComponent;
  let fixture: ComponentFixture<TopClinicListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopClinicListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopClinicListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
