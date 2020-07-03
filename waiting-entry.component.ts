import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { WaitingListEntry } from '../model/waiting-list-entry';

@Component({
  selector: 'app-waiting-entry',
  templateUrl: './waiting-entry.component.html',
  styleUrls: ['./waiting-entry.component.css']
})
export class WaitingEntryComponent {
  @Input()
  public data: WaitingListEntry;

  @Output()
  public delete = new EventEmitter<WaitingListEntry>();
}
