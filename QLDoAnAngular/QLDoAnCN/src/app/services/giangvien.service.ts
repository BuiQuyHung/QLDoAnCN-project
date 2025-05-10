import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GiangVien } from '../models/giangvien.model';
import { ChuyenNganh } from '../models/chuyennganh.model';

@Injectable({
  providedIn: 'root'
})
export class GiangVienService {
  private apiUrl = 'https://localhost:7121/api/GiangViens'; // API gốc cho Giảng Viên
  private chuyenNganhUrl = 'https://localhost:7121/api/ChuyenNganhs'; // API cho Chuyên Ngành

  constructor(private http: HttpClient) { }

  getAllGiangVien(): Observable<GiangVien[]> {
    return this.http.get<GiangVien[]>(this.apiUrl);
  }

  getGiangVien(id: string): Observable<GiangVien> {
    return this.http.get<GiangVien>(`${this.apiUrl}/\${id}`);
  }

  addGiangVien(giangVien: GiangVien): Observable<GiangVien> {
    return this.http.post<GiangVien>(this.apiUrl, giangVien);
  }

  updateGiangVien(id: string, giangVien: GiangVien): Observable<any> {
    return this.http.put(`${this.apiUrl}/\${id}`, giangVien);
  }

  deleteGiangVien(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/\${id}`);
  }

  getAllChuyenNganh(): Observable<string[]> {
    return this.http.get<string[]>(this.chuyenNganhUrl);
  }
}