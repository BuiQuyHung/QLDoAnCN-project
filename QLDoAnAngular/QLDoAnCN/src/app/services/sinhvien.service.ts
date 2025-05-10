import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SinhVien } from '../models/sinhvien.model';

@Injectable({
  providedIn: 'root'
})
export class SinhVienService {
  private apiUrl = 'https://localhost:7121/api/SinhViens'; 

  constructor(private http: HttpClient) { }

  getAllSinhVien(): Observable<SinhVien[]> {
    return this.http.get<SinhVien[]>(this.apiUrl);
  }

  getSinhVien(id: string): Observable<SinhVien> {
    return this.http.get<SinhVien>(`${this.apiUrl}/${id}`);
  }


  addSinhVien(sinhVien: SinhVien): Observable<SinhVien> {
    return this.http.post<SinhVien>(this.apiUrl, sinhVien);
  }

  updateSinhVien(id: string, sinhVien: SinhVien): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, sinhVien);
  }

  deleteSinhVien(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}