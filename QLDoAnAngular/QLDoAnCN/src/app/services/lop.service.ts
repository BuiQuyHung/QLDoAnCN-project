import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lop } from '../models/lop.model';

@Injectable({
  providedIn: 'root'
})
export class LopService {
  private apiUrl = 'https://localhost:7121/api/Lops';

  constructor(private http: HttpClient) { }

  getAllLop(): Observable<Lop[]> {
    return this.http.get<Lop[]>(this.apiUrl);
  }

  getLop(id: string): Observable<Lop> {
    return this.http.get<Lop>(`${this.apiUrl}/${id}`);
  }

  addLop(lop: Lop): Observable<Lop> {
    return this.http.post<Lop>(this.apiUrl, lop);
  }

  updateLop(id: string, lop: Lop): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, lop);
  }

  deleteLop(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}