import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { of, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { WaitingListEntry } from '../model/waiting-list-entry';
import { AmbulanceWaitingListService } from '../ambulance-waiting-list.service';

@Component({
  selector: 'app-waiting-entry-editor',
  templateUrl: './waiting-entry-editor.component.html',
  styleUrls: ['./waiting-entry-editor.component.css']
})
export class WaitingEntryEditorComponent implements OnInit {
  private static newEntryPlaceholder: WaitingListEntry = {
    id: undefined,
    name: '',
    patientId: '',
    since: new Date(Date.now()),
    estimated: undefined,
    estimatedDurationMinutes: 20,
    condition: {
      code: 'nausea',
      name: 'Nevoľnosť',
      reference: ''
    }
  };
  public readonly knownConditions$: Observable<Array<{ concept: string, display: string }>>;

  public data$: Observable<WaitingListEntry>;
  constructor(
    private readonly route: ActivatedRoute,
    private service: AmbulanceWaitingListService,
    private router: Router) {
    this.knownConditions$ = of([
      { concept: 'folowup', display: 'Kontrola' },
      { concept: 'nausea', display: 'Nevoľnosť' },
      { concept: 'fever', display: 'Teploty' },
      { concept: 'ache-in-throat', display: 'Bolesti hrdla' }
    ]);
  }
  public ngOnInit(): void {
    this.data$ = this.route.paramMap.pipe(
      map(_ => _.get('id')),
      switchMap(
        id => (id === 'new')
          ? of(WaitingEntryEditorComponent.newEntryPlaceholder)
          : this.service.getWaitingEntry(id)
      )
    );
  }

  public save(data: WaitingListEntry): void {
    this.service.save(data);
    this.router.navigate(['/waiting-list']);
  }
}
