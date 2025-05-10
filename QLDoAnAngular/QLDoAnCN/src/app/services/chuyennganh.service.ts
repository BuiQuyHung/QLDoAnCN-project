import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChuyenNganh } from '../models/chuyennganh.model';

@Injectable({
  providedIn: 'root'
})
export class ChuyenNganhService {
  private apiUrl = 'https://localhost:7121/api/ChuyenNganhs';

  constructor(private http: HttpClient) { }

  getAllChuyenNganh(): Observable<ChuyenNganh[]> {
    return this.http.get<ChuyenNganh[]>(this.apiUrl);
  }

  getChuyenNganh(id: string): Observable<ChuyenNganh> {
    return this.http.get<ChuyenNganh>(`${this.apiUrl}/${id}`);
  }

  addChuyenNganh(chuyenNganh: ChuyenNganh): Observable<ChuyenNganh> {
    return this.http.post<ChuyenNganh>(this.apiUrl, chuyenNganh);
  }

  updateChuyenNganh(id: string, chuyenNganh: ChuyenNganh): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, chuyenNganh);
  }

  deleteChuyenNganh(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}