import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AmbulanceListComponent } from './ambulance-list.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { empty, of, Observable } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { WaitingListEntry } from '../model/waiting-list-entry';
import { AmbulanceWaitingListService } from '../ambulance-waiting-list.service';

@Component({ selector: 'app-waiting-entry', template: '' })
class WaitingEntryStubComponent {
  @Input() data: WaitingListEntry[] = [];
}

class AmbulanceWaitingListServiceMock {
  getAllWaitingEntries(ambulanceId: string): Observable<WaitingListEntry[]> {
    return of([]);
  }
}

describe('AmbulanceListComponent', () => {
  let component: AmbulanceListComponent;
  let fixture: ComponentFixture<AmbulanceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MatExpansionModule, MatIconModule, RouterTestingModule],
      declarations: [AmbulanceListComponent, WaitingEntryStubComponent],
      providers: [{
        provide: AmbulanceWaitingListService,
        useClass: AmbulanceWaitingListServiceMock
      }]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulanceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => expect(component).toBeTruthy());
});
