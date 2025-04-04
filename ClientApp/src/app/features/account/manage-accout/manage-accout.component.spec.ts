import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAccoutComponent } from './manage-accout.component';

describe('ManageAccoutComponent', () => {
  let component: ManageAccoutComponent;
  let fixture: ComponentFixture<ManageAccoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageAccoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAccoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
