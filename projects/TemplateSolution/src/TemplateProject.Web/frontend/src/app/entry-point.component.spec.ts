import { TestBed, async } from '@angular/core/testing';
import { EntryPointComponent } from './entry-point.component';
describe('EntryPointComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        EntryPointComponent
      ],
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(EntryPointComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
  it(`should have as title 'TemplateProject'`, async(() => {
    const fixture = TestBed.createComponent(EntryPointComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('TemplateProject');
  }));
  it('should render title in a h1 tag', async(() => {
    const fixture = TestBed.createComponent(EntryPointComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Welcome to TemplateProject!');
  }));
});
