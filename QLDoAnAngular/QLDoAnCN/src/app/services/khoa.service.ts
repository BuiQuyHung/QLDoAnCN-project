import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Khoa } from '../models/khoa.model';

@Injectable({
  providedIn: 'root'
})
export class KhoaService {
  private apiUrl = 'https://localhost:7121/api/Khoas'; // API gốc cho Khoa

  constructor(private http: HttpClient) { }

  getAllKhoa(): Observable<Khoa[]> {
    return this.http.get<Khoa[]>(this.apiUrl);
  }

  getKhoa(id: string): Observable<Khoa> {
    return this.http.get<Khoa>(`${this.apiUrl}/${id}`);
  }

  addKhoa(khoa: Khoa): Observable<Khoa> {
    return this.http.post<Khoa>(this.apiUrl, khoa);
  }

  updateKhoa(id: string, khoa: Khoa): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, khoa);
  }

  deleteKhoa(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}