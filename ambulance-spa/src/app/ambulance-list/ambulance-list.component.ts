import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AmbulanceWaitingListService } from '../ambulance-waiting-list.service';
import { WaitingListEntry } from '../model/waiting-list-entry';

@Component({
  selector: 'app-ambulance-list',
  templateUrl: './ambulance-list.component.html',
  styleUrls: ['./ambulance-list.component.css']
})
export class AmbulanceListComponent implements OnInit {
  private static readonly MINUTE = 60 * 1000;
  public waitingList: Observable<WaitingListEntry[]>;

  constructor(private service: AmbulanceWaitingListService) {
    this.waitingList = service.getAllWaitingEntries();
  }

  ngOnInit(): void { }

  onDelete(data: WaitingListEntry): void {
    this.service.deleteEntry(data.id).subscribe(() => {
      this.waitingList = this.service.getAllWaitingEntries();
    });
  }
}
