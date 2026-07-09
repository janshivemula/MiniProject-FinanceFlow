import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentBudgetForm } from './department-budget-form';

describe('DepartmentBudgetForm', () => {
  let component: DepartmentBudgetForm;
  let fixture: ComponentFixture<DepartmentBudgetForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentBudgetForm],
    }).compileComponents();

    fixture = TestBed.createComponent(DepartmentBudgetForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
