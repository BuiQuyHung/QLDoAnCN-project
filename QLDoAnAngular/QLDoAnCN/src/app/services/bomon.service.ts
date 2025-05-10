import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BoMon } from '../models/bomon.model';

@Injectable({
  providedIn: 'root'
})
export class BoMonService {
  private apiUrl = 'https://localhost:7121/api/BoMons';

  constructor(private http: HttpClient) { }

  getAllBoMons(): Observable<BoMon[]> {
    return this.http.get<BoMon[]>(this.apiUrl);
  }

  getBoMon(id: string): Observable<BoMon> {
    return this.http.get<BoMon>(`${this.apiUrl}/${id}`);
  }

  addBoMon(boMon: BoMon): Observable<BoMon> {
    return this.http.post<BoMon>(this.apiUrl, boMon);
  }

  updateBoMon(id: string, boMon: BoMon): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, boMon);
  }

  deleteBoMon(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}