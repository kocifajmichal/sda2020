import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { WaitingListEntry } from './model/waiting-list-entry';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class AmbulanceWaitingListService {

  private readonly AmbulanceId = '100';
  private readonly BaseUrl = `https://localhost:5001/developers/ambulance/${this.AmbulanceId}`;

  public constructor(private httpClient: HttpClient) { }

  public getAllWaitingEntries(): Observable<WaitingListEntry[]> {
    const url = `${this.BaseUrl}/entry`;
    const apiCall = this.httpClient.get(url);
    return apiCall.pipe(map(response => (response as WaitingListEntry[])));
  }

  public getWaitingEntry(id: string): Observable<WaitingListEntry> {
    return this.httpClient.get(this.BaseUrl + '/entry/' + id).pipe(map(response => response as WaitingListEntry));
  }

  public deleteEntry(id: string): Observable<any> {
    return this.httpClient.delete(this.BaseUrl + '/entry/' + id);
  }

  save(entry: WaitingListEntry): void {
    this.httpClient.post(this.BaseUrl + '/entry', entry).subscribe();
  }
}
