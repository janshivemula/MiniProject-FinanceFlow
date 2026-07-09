import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseClaimForm } from './expense-claim-form';

describe('ExpenseClaimForm', () => {
  let component: ExpenseClaimForm;
  let fixture: ComponentFixture<ExpenseClaimForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpenseClaimForm],
    }).compileComponents();

    fixture = TestBed.createComponent(ExpenseClaimForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
