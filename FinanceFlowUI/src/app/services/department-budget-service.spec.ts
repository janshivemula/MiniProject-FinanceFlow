import { TestBed } from '@angular/core/testing';

import { DepartmentBudgetService } from './department-budget-service';

describe('DepartmentBudgetService', () => {
  let service: DepartmentBudgetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartmentBudgetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
