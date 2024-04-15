# Networking

networking.md


```
Aspire.Hosting.Dcp.dcpctrl.ServiceReconciler
```

```
netstat -anvp tcp | awk 'NR<3 || /LISTEN/'
```

```
lsof -i tcp:5303
```

```
netstat -anp tcp | grep 5303
```

```
COMMAND   PID   USER   FD   TYPE             DEVICE SIZE/OFF NODE NAME
dcpctrl 96234 moljac   15u  IPv6 0x210bb6ffacf7543b      0t0  TCP localhost:hacl-probe (LISTEN)
dcpctrl 96234 moljac   16u  IPv4 0x210bb6fab1a2ec43      0t0  TCP localhost:hacl-probe (LISTEN)
```

```
netstat -anp tcp | grep 5303
tcp4       0      0  127.0.0.1.5303         *.*                    LISTEN     
tcp6       0      0  ::1.5303               *.*                    LISTEN     
```

```
sudo service stop dcpctrl
sudo service start dcpctrl
```


