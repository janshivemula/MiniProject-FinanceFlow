import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentBudgetList } from './department-budget-list';

describe('DepartmentBudgetList', () => {
  let component: DepartmentBudgetList;
  let fixture: ComponentFixture<DepartmentBudgetList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentBudgetList],
    }).compileComponents();

    fixture = TestBed.createComponent(DepartmentBudgetList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
