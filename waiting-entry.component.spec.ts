import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WaitingEntryComponent } from './waiting-entry.component';

describe('WaitingEntryComponent', () => {
  let component: WaitingEntryComponent;
  let fixture: ComponentFixture<WaitingEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [WaitingEntryComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WaitingEntryComponent);
    component = fixture.componentInstance;
    component.data = {
      id: '1',
      name: 'Jožko Púčik',
      patientId: '100.01',
      since: new Date(),
      estimated: new Date(),
      estimatedDurationMinutes: 15,
      condition: 'Kontrola'
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
