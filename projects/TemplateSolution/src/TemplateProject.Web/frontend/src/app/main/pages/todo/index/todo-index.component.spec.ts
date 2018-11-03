import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TodoIndexComponent } from './todo-index.component';

describe('TodoIndexComponent', () => {
  let component: TodoIndexComponent;
  let fixture: ComponentFixture<TodoIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TodoIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TodoIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
