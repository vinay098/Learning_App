import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Skill } from '../interface/skill';
import { ShowSkills } from '../interface/show-skills';
import * as forge from 'node-forge';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {

  private skillUrl = 'http://localhost:5100/api/Skills/';
  private publicKey = `-----BEGIN PUBLIC KEY-----
  MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgGoZLL6Sr28XgSsr1ThmNqEmfW+b
  s/QAdUaAyFiGdtaeLNBjveJ46KQk1crTbutHfDK0u2fT0FGca4NmwKKE2Bgx+5HO
  knyIHZSwdltq7iFXvpCm5qctr+EZ0NZym1z88Ps6+sWtlXmpIbn/RCh0//duGywu
  sqBg01xwHSaB5eafAgMBAAE=
  -----END PUBLIC KEY-----`;
  private privateKey = `-----BEGIN RSA PRIVATE KEY-----
  MIICWwIBAAKBgGoZLL6Sr28XgSsr1ThmNqEmfW+bs/QAdUaAyFiGdtaeLNBjveJ4
  6KQk1crTbutHfDK0u2fT0FGca4NmwKKE2Bgx+5HOknyIHZSwdltq7iFXvpCm5qct
  r+EZ0NZym1z88Ps6+sWtlXmpIbn/RCh0//duGywusqBg01xwHSaB5eafAgMBAAEC
  gYAFCbz+L6j09YDEtAgj7XaaoGFEc3hQFdV7y5G34mqyNkCahKH3pxYk9TrRrsiN
  k49qOPrNK1mxBmR0kOSE0xoPvCB1oDy9+2eT06+LFl6mNCfdSyHVXWgjeqaWiGOj
  iSEt38fVDAsNAVTne4LuC8nBD8aw1yczmXAbXlSj55QeQQJBAMdLX5t9c92GQ6TO
  BGuiDtWjCItlFeDLQCDN2v3vSD3gQMW0gPs4DgK4O73I35BL9tPeTOBXA1SQH9CJ
  aFfmBlUCQQCISWN/4oG+iObSyLADGla4OwBK9gKOQAgQiHqNgFLflRaDSIDsRPwS
  LtsDbAU2+1YHgJsKkgJXo5Tv3UQRpeUjAkBxfbnXaUy3JUOWMYAQ7zu1a19tSkZ4
  Oiatx6zjGQWxvugD7nPZHCXWZKYYwLCXkrrgEmMDCmoqGN0VO3rBO4v1AkA+hXRY
  6Z9duk4x6pEci0u3LpH+0YbwnR1WAFZAbdsW6L0wMuW5/hepe8dLdZWa2Ihp3qzB
  l6PtcoTT2Szt764DAkEAin3u4/G0EqEW2eKr/jkiDZk917m4cfKtsg1P0V6AELqj
  tV5zK4gOpwNlXt5piVLClFlYe3tsIHswbiY3FjzclQ==
  -----END RSA PRIVATE KEY-----`;

  constructor(private http: HttpClient) { }

  // decryptData(encryptedData, privateKeyPem) {
  //   try {
  //     const privateKey = forge.pki.privateKeyFromPem(privateKeyPem);
  //     const base64EncodedData = Buffer.from(encryptedData, 'utf8').toString('base64'); // Convert Buffer to base64 string
  //     const decryptedBytes = privateKey.decrypt(base64EncodedData);
  
  //     // Check if decryption failed (might return null)
  //     if (decryptedBytes === null) {
  //       throw new Error('Decryption failed.');
  //     }
  
  //     const decoder = new TextDecoder('utf-8'); // Or any other appropriate encoding
  //     return decoder.decode(decryptedBytes);
  //   } catch (err) {
  //     console.error('Error during decryption:', err);
  //     // Handle decryption errors appropriately
  //     return null;
  //   }
  // }

  addSkill(skill: Skill) {
    var rsa = forge.pki.publicKeyFromPem(this.publicKey);
    var encryptedName = window.btoa(rsa.encrypt(String(skill.Name)));
    var encryptedFamily = window.btoa(rsa.encrypt(String(skill.Family)));
   
    const obj = {
      Name: encryptedName,
      Family: encryptedFamily
    }
    console.log("Encrypted name = " + obj.Name + " " + "Encrypted family =" + obj.Family);
    return this.http.post(this.skillUrl + 'add-skills', obj);
  }

  getSkills() {
    return this.http.get<ShowSkills[]>(this.skillUrl + 'get-skills');
  }

  updateSkills(id: number, skill: Skill) {
    return this.http.put<Skill>(this.skillUrl + "update-skill/" + id, skill);
  }

  getskill(id: number) {
    return this.http.get<ShowSkills>(this.skillUrl + "get-skill/" + id);
  }

  deleteSkill(id: number) {
    return this.http.delete(this.skillUrl + "delete-skills/" + id);
  }
}
