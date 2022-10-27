import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepChangeComponent } from './step-change.component';

describe('StepChangeComponent', () => {
  let component: StepChangeComponent;
  let fixture: ComponentFixture<StepChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StepChangeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StepChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
