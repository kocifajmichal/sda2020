import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, MatIconModule, MatToolbarModule],
      declarations: [AppComponent],
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
  it('should have toolbar and icon', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const approot: HTMLElement = fixture.debugElement.nativeElement;
    const toolbar: HTMLElement = approot.querySelector('mat-toolbar');
    expect(toolbar.querySelector('mat-icon')).toBeDefined();
  });
});
