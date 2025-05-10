import { Component, OnInit } from '@angular/core';
import { ChuyenNganhService } from '../services/chuyennganh.service';
import { ChuyenNganh } from '../models/chuyennganh.model';
import { NganhService } from '../services/nganh.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PopupComponent } from '../shared/popup/popup.component';

@Component({
  selector: 'app-chuyen-nganh',
  standalone: true,
  imports: [CommonModule, FormsModule, PopupComponent],
  templateUrl: './chuyennganh.component.html',
  styleUrl: './chuyennganh.component.css'
})
export class ChuyenNganhComponent implements OnInit {
  danhSachChuyenNganh: ChuyenNganh[] = [];
  selectedChuyenNganh: ChuyenNganh | null = null;
  isAddingNew: boolean = false;
  newChuyenNganh: ChuyenNganh = { maChuyenNganh: '', tenChuyenNganh: '', maNganh: '' }; 
  isEditing: boolean = false;
  editingChuyenNganh: ChuyenNganh | null = null;
  errorMessage: string = '';
  successMessage: string = '';
  isViewingDetails: boolean = false;
  nganhs: { maNganh: string, tenNganh: string }[] = []; 

  constructor(private chuyenNganhService: ChuyenNganhService, private nganhService: NganhService) { }

  ngOnInit(): void {
    this.loadDanhSachChuyenNganh();
    this.loadDanhSachNganh(); 
  }

  loadDanhSachChuyenNganh(): void {
    this.chuyenNganhService.getAllChuyenNganh().subscribe({
      next: (data) => {
        this.danhSachChuyenNganh = data;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách chuyên ngành.';
        console.error('Lỗi tải danh sách chuyên ngành', error);
      }
    });
  }

  loadDanhSachNganh(): void {
    this.nganhService.getAllNganh().subscribe({
      next: (data) => {
        this.nganhs = data; 
      },
      error: (error) => {
        console.error('Lỗi khi tải danh sách ngành', error);
        this.errorMessage = 'Lỗi khi tải danh sách ngành.';
      }
    });
  }

  hienThiChiTiet(chuyenNganh: ChuyenNganh): void {
    this.selectedChuyenNganh = chuyenNganh;
    this.isViewingDetails = true;
  }

  batDauThemMoi(): void {
    this.isAddingNew = true;
    this.newChuyenNganh = { maChuyenNganh: '', tenChuyenNganh: '', maNganh: '' };
  }

  batDauChinhSua(chuyenNganh: ChuyenNganh): void {
    this.editingChuyenNganh = { ...chuyenNganh };
    this.isEditing = true;
  }

  dongPopupChiTiet(): void {
    this.isViewingDetails = false;
    this.selectedChuyenNganh = null;
  }

  dongPopupThemMoi(): void {
    this.isAddingNew = false;
  }

  dongPopupChinhSua(): void {
    this.isEditing = false;
    this.editingChuyenNganh = null;
  }

  themChuyenNganh(): void {
    this.chuyenNganhService.addChuyenNganh(this.newChuyenNganh).subscribe({
      next: (response) => {
        this.successMessage = 'Thêm chuyên ngành thành công.';
        this.loadDanhSachChuyenNganh();
        this.isAddingNew = false;
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi thêm chuyên ngành.';
        console.error('Lỗi thêm chuyên ngành', error);
      }
    });
  }

  capNhatChuyenNganh(): void {
    if (this.editingChuyenNganh) {
      this.chuyenNganhService.updateChuyenNganh(this.editingChuyenNganh.maChuyenNganh, this.editingChuyenNganh).subscribe({
        next: (response) => {
          this.successMessage = 'Cập nhật chuyên ngành thành công.';
          this.loadDanhSachChuyenNganh();
          this.isEditing = false;
          this.editingChuyenNganh = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi cập nhật chuyên ngành.';
          console.error('Lỗi cập nhật chuyên ngành', error);
        }
      });
    }
  }

  huyThemMoi(): void {
    this.isAddingNew = false;
  }

  huyChinhSua(): void {
    this.isEditing = false;
    this.editingChuyenNganh = null;
  }

  xoaChuyenNganh(maChuyenNganh: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa chuyên ngành này?')) {
      this.chuyenNganhService.deleteChuyenNganh(maChuyenNganh).subscribe({
        next: (response) => {
          this.successMessage = 'Xóa chuyên ngành thành công.';
          this.loadDanhSachChuyenNganh();
          this.selectedChuyenNganh = null;
          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => {
          this.errorMessage = 'Lỗi khi xóa chuyên ngành.';
          console.error('Lỗi xóa chuyên ngành', error);
        }
      });
    }
  }
}