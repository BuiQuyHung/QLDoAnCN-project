import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Nganh } from '../models/nganh.model';


@Injectable({
  providedIn: 'root'
})
export class NganhService {
  private apiUrl = 'https://localhost:7121/api/Nganhs'; // API gốc cho Ngành

  constructor(private http: HttpClient) { }

  getAllNganh(): Observable<Nganh[]> {
    return this.http.get<Nganh[]>(this.apiUrl);
  }

  getNganh(id: string): Observable<Nganh> {
    return this.http.get<Nganh>(`${this.apiUrl}/${id}`);
  }

  addNganh(nganh: Nganh): Observable<Nganh> {
    return this.http.post<Nganh>(this.apiUrl, nganh);
  }

  updateNganh(id: string, nganh: Nganh): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, nganh);
  }

  deleteNganh(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}