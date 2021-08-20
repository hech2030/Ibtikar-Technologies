import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsOrderComponent } from './products-order.component';

describe('ProductsOrderComponent', () => {
  let component: ProductsOrderComponent;
  let fixture: ComponentFixture<ProductsOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductsOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductsOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
