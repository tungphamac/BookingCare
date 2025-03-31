import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopSpecializationListComponent } from './top-specialization-list.component';

describe('TopSpecializationListComponent', () => {
  let component: TopSpecializationListComponent;
  let fixture: ComponentFixture<TopSpecializationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopSpecializationListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopSpecializationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
