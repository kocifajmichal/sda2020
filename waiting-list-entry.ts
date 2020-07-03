export interface WaitingListEntry {
  id: string;
  name: string;
  patientId: string;
  since: Date;
  estimated: Date;
  estimatedDurationMinutes: number;
  condition: {
    code: string;
    name: string;
    reference: string;
  };
}
