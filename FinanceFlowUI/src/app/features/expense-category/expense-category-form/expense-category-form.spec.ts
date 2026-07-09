import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseCategoryForm } from './expense-category-form';

describe('ExpenseCategoryForm', () => {
  let component: ExpenseCategoryForm;
  let fixture: ComponentFixture<ExpenseCategoryForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpenseCategoryForm],
    }).compileComponents();

    fixture = TestBed.createComponent(ExpenseCategoryForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
