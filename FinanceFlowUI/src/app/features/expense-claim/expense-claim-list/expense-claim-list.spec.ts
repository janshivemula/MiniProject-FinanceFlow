import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseClaimList } from './expense-claim-list';

describe('ExpenseClaimList', () => {
  let component: ExpenseClaimList;
  let fixture: ComponentFixture<ExpenseClaimList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpenseClaimList],
    }).compileComponents();

    fixture = TestBed.createComponent(ExpenseClaimList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
