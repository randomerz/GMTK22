ftc = open('Act3/Optimist/01_IveGotIt.asset', 'rb')
by = ftc.read()
while (t:=input('\n')) != 'q':
    f = open(t+'.asset','wb')
    f.write(by)
    f.close()

