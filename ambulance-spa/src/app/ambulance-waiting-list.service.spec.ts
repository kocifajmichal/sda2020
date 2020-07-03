import { TestBed } from '@angular/core/testing';

import { AmbulanceWaitingListService } from './ambulance-waiting-list.service';

describe('AmbulanceWaitingListService', () => {
  let service: AmbulanceWaitingListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AmbulanceWaitingListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
